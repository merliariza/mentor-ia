import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { forkJoin, map, Observable, of, switchMap } from 'rxjs';
import { environment } from '../../../environments/environment';

interface UserMember {
  id: number;
  fullName?: string;
  name?: string;
  email?: string;
}

interface Progress {
  id: number;
  userMemberId: number;
  topic: string;
  score: number;
  feedback: string;
}

interface EvaluationSession {
  id: number;
  progressId: number;
  score: number;
  feedback: string;
}

interface Flashcard {
  id: number;
  question: string;
  answer: string;
  evaluationSessionId: number;
}

export interface DashboardStats {
  topics: number;
  averageScore: number;
  evaluations: number;
  flashcards: number;
}

export interface DashboardSession {
  id: number;
  topic: string;
  score: number;
  feedback: string;
  progressId: number;
}

export interface DashboardData {
  userName: string;
  stats: DashboardStats;
  currentTopic: Progress | null;
  recentSessions: DashboardSession[];
  recommendation: string;
}

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = environment.apiUrl;

  getDashboard(): Observable<DashboardData> {
    const user = this.getStoredUser();
    const userId = this.getUserId();

    if (!userId) {
      return of({
        userName: user?.Name ?? 'Estudiante',
        stats: {
          topics: 0,
          averageScore: 0,
          evaluations: 0,
          flashcards: 0,
        },
        currentTopic: null,
        recentSessions: [],
        recommendation: 'Comienza una sesión con Mentor-IA para empezar a construir tu progreso.',
      });
    }

    return this.http.get<UserMember[]>(`${this.apiUrl}/UserMember`).pipe(
      switchMap((members) => {
        const member = members.find((item) => item.id === userId);

        return this.http
          .get<Progress[]>(`${this.apiUrl}/Progress`)
          .pipe(
            switchMap((progresses) => {
              const userProgress = progresses.filter(
                (progress) => progress.userMemberId === userId
              );

              if (userProgress.length === 0) {
                return of({
                  member,
                  progress: [],
                  sessions: [],
                  flashcards: [],
                });
              }

              const sessionRequests = userProgress.map((progress) =>
                this.http.get<EvaluationSession[]>(
                  `${this.apiUrl}/EvaluationSession`
                ).pipe(
                  map((sessions) =>
                    sessions.filter(
                      (session) => session.progressId === progress.id
                    )
                  )
                )
              );

              return forkJoin(sessionRequests).pipe(
                switchMap((sessionGroups) => {
                  const sessions = sessionGroups.flat();

                  if (sessions.length === 0) {
                    return of({
                      member,
                      progress: userProgress,
                      sessions: [],
                      flashcards: [],
                    });
                  }

                  return this.http
                    .get<Flashcard[]>(`${this.apiUrl}/Flashcard`)
                    .pipe(
                      map((flashcards) => ({
                        member,
                        progress: userProgress,
                        sessions,
                        flashcards,
                      }))
                    );
                })
              );
            })
          );
      }),
      map(({ member, progress, sessions, flashcards }) => {
        const orderedProgress = [...progress].sort((a, b) => b.id - a.id);
        const orderedSessions = [...sessions].sort((a, b) => b.id - a.id);

        const averageScore =
          progress.length > 0
            ? Math.round(
                progress.reduce((total, item) => total + item.score, 0) /
                  progress.length
              )
            : 0;

        const currentTopic = orderedProgress[0] ?? null;

        const recentSessions = orderedSessions
          .slice(0, 5)
          .map((session) => {
            const relatedProgress = progress.find(
              (item) => item.id === session.progressId
            );

            return {
              id: session.id,
              topic: relatedProgress?.topic ?? 'Evaluación',
              score: session.score,
              feedback: session.feedback,
              progressId: session.progressId,
            };
          });

        return {
          userName:
            member?.fullName ??
            member?.name ??
            this.getStoredUser()?.Name ??
            'Estudiante',
          stats: {
            topics: progress.length,
            averageScore,
            evaluations: sessions.length,
            flashcards: flashcards.filter((flashcard) =>
              sessions.some(
                (session) => session.id === flashcard.evaluationSessionId
              )
            ).length,
          },
          currentTopic,
          recentSessions,
          recommendation: this.buildRecommendation(
            averageScore,
            currentTopic,
            progress.length
          ),
        };
      })
    );
  }

  private buildRecommendation(
    averageScore: number,
    currentTopic: Progress | null,
    topics: number
  ): string {
    if (!currentTopic && topics === 0) {
      return 'Comienza una conversación con Mentor-IA para aprender tu primer tema.';
    }

    if (averageScore < 70) {
      return `Te recomendamos reforzar "${currentTopic?.topic ?? 'tus temas actuales'}". Tus resultados indican que puedes beneficiarte de una nueva sesión de práctica.`;
    }

    if (averageScore < 85) {
      return `Vas por buen camino con "${currentTopic?.topic ?? 'tus temas actuales'}". Intenta realizar otra evaluación para reforzar lo aprendido.`;
    }

    return `Excelente progreso. Puedes continuar profundizando en "${currentTopic?.topic ?? 'el tema que estás estudiando'}" o comenzar un nuevo tema.`;
  }

  private getStoredUser(): any {
    const storedUser = localStorage.getItem('user');

    if (!storedUser) {
      return null;
    }

    try {
      return JSON.parse(storedUser);
    } catch {
      return null;
    }
  }

  private getUserId(): number | null {
    const storedUser = this.getStoredUser();

    if (storedUser?.Id) {
      return Number(storedUser.Id);
    }

    if (storedUser?.id) {
      return Number(storedUser.id);
    }

    const token = localStorage.getItem('token');

    if (!token) {
      return null;
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const id =
        payload.id ??
        payload.sub ??
        payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

      return id ? Number(id) : null;
    } catch {
      return null;
    }
  }
}