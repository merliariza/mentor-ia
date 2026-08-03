import { Component, Input } from '@angular/core';
import { MarkdownModule } from 'ngx-markdown';

import { ChatMessage } from '../../../../core/models/chat-message';

@Component({
  selector: 'app-conversation',
  standalone: true,
  imports: [
    MarkdownModule
  ],
  templateUrl: './conversation.html',
  styleUrl: './conversation.css'
})
export class ConversationComponent {

  @Input()
  messages: ChatMessage[] = [];

}