import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-chat-input',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './chat-input.html',
  styleUrl: './chat-input.css'
})
export class ChatInputComponent {

  message = '';

  @Output()
  send = new EventEmitter<string>();

  sendMessage() {

    const text = this.message.trim();

    if (!text) return;

    this.send.emit(text);

    this.message = '';

  }

}