import { Component, inject } from '@angular/core';
import { ConversationComponent } from '../../components/conversation/conversation';
import { ChatInputComponent } from '../../components/chat-input/chat-input';
import { ChatMessage } from '../../../../core/models/chat-message';
import { ChatService } from '../../../../core/services/chat.service';
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
private readonly chatService = inject(ChatService);
  messages: ChatMessage[] = [
    {
      sender: 'assistant',
      content: '¡Hola! Soy Mentor-IA. ¿Qué tema deseas aprender hoy?'
    }
  ];

  isLoading = false;

addMessage(message: string) {

  this.messages.push({
    sender: 'user',
    content: message
  });

  this.isLoading = true;

  this.chatService.sendMessage({

    question: message,

    user: {
      id: 1,
      fullName: 'Merli'
    }

  }).subscribe({

    next: (response) => {

      this.messages.push({
        sender: 'assistant',
        content: response.answer
      });

      this.isLoading = false;

    },

    error: (error) => {

      console.error(error);

      this.messages.push({
        sender: 'assistant',
        content: 'Lo siento, ocurrió un error al conectar con Mentor-IA.'
      });

      this.isLoading = false;

    }

  });

}
}