import { Injectable, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, tap } from 'rxjs';

import { ChatRequest } from '../models/chat-request';
import { ChatResponse } from '../models/chat-response';
import { ChatMessage } from '../models/chat-message';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private readonly http = inject(HttpClient);

  private readonly api =
    'http://localhost:5253/api/AI/chat';

  readonly messages =
    signal<ChatMessage[]>([]);

  readonly lastTopic =
    signal('');

  sendMessage(
    request: ChatRequest
  ): Observable<ChatResponse> {

    this.messages.update(messages => [

      ...messages,

      {

        sender: 'user',

        content: request.question

      }

    ]);

    return this.http
      .post<ChatResponse>(
        this.api,
        request
      )
      .pipe(

        tap(response => {

          this.messages.update(messages => [

            ...messages,

            {

              sender: 'assistant',

              content: response.answer

            }

          ]);

          this.lastTopic.set(
            response.topic ??
            request.question
          );

        })

      );

  }

  clearConversation() {

    this.messages.set([]);

    this.lastTopic.set('');

  }

}