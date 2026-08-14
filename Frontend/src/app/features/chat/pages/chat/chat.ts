import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

import { ConversationComponent } from '../../components/conversation/conversation';
import { ChatInputComponent } from '../../components/chat-input/chat-input';

import { ChatMessage } from '../../../../core/models/chat-message';
import { ChatResponse } from '../../../../core/models/chat-response';

import { ChatService } from '../../../../core/services/chat.service';
import { AuthService } from '../../../../features/auth/services/auth.service';

import { QuizService } from '../../../quiz/services/quiz.service';
import { QuizStateService } from '../../../../core/services/quiz-state.service';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [
    ConversationComponent,
    ChatInputComponent
  ],
  templateUrl: './chat.html',
  styleUrl: './chat.css'
})
export class ChatComponent {

  private readonly chatService =
    inject(ChatService);

  private readonly auth =
    inject(AuthService);

  private readonly quizService =
    inject(QuizService);

  readonly quizState =
    inject(QuizStateService);

  private readonly router =
    inject(Router);


  messages: ChatMessage[] = [

    {
      sender: 'assistant',
      content:
        '¡Hola! Soy Mentor-IA. ¿Qué tema deseas aprender hoy?'
    }

  ];


  lastResponse: ChatResponse | null = null;

  isLoading = false;


  addMessage(message: string) {

    const currentUser =
      this.auth.currentUser();

    const userId =
      this.auth.getCurrentUserId();


    if (!currentUser || userId === null) {

      this.messages.push({

        sender: 'assistant',

        content:
          'No se pudo identificar al usuario actual. Por favor, inicia sesión nuevamente.'

      });

      return;

    }


    this.messages.push({

      sender: 'user',

      content: message

    });


    this.isLoading = true;


    this.chatService.sendMessage({

      question: message,

      user: {

        id: userId,

        fullName:
          currentUser.name ||
          currentUser.userName ||
          currentUser.email

      }

    })
    .subscribe({

      next: response => {

        this.lastResponse = response;


        this.messages.push({

          sender: 'assistant',

          content: response.answer

        });


        this.isLoading = false;

      },


      error: () => {

        this.messages.push({

          sender: 'assistant',

          content:
            'Lo siento, ocurrió un error al conectar con Mentor-IA.'

        });


        this.isLoading = false;

      }

    });

  }


  generateQuiz() {

    if (!this.lastResponse?.progressId) {

      return;

    }


    this.quizState.loading.set(true);


    this.quizService
      .generateQuiz(this.lastResponse.progressId)
      .subscribe({

        next: questions => {

          this.quizState.setQuiz(

            this.lastResponse!.progressId!,

            this.lastResponse!.topic ?? '',

            questions

          );


          this.quizState.loading.set(false);


          this.router.navigate(['/quiz']);

        },


        error: () => {

          this.quizState.loading.set(false);

        }

      });

  }


  generateFlashcards() {

    if (!this.lastResponse) {

      return;

    }

  }

}