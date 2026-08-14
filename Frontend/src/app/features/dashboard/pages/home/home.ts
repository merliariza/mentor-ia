import { Component, OnInit, inject, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { DashboardService, DashboardData } from '../../../../core/services/dashboard.service';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class HomeComponent implements OnInit {
  private readonly dashboardService = inject(DashboardService);
  private readonly router = inject(Router);

  readonly dashboard = signal<DashboardData | null>(null);
  readonly loading = signal(true);
  readonly error = signal(false);

  ngOnInit(): void {
    this.loadDashboard();
  }

  loadDashboard(): void {
    this.loading.set(true);
    this.error.set(false);

    this.dashboardService.getDashboard().subscribe({
      next: (data) => {
        this.dashboard.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set(true);
        this.loading.set(false);
      },
    });
  }

  navigateToChat(): void {
    this.router.navigate(['/chat']);
  }

  navigateToQuiz(): void {
    this.router.navigate(['/quiz']);
  }

  navigateToFlashcards(): void {
    this.router.navigate(['/flashcards']);
  }

  getProgressWidth(score: number): string {
    return `${Math.min(Math.max(score, 0), 100)}%`;
  }
}