import { Injectable, signal } from '@angular/core';

import { Flashcard } from '../../features/flashcards/models/flashcard';

@Injectable({
  providedIn: 'root'
})
export class FlashcardStateService {

  readonly progressId =
    signal<number | null>(null);

  readonly topic =
    signal('');

  readonly cards =
    signal<Flashcard[]>([]);

  readonly current =
    signal(0);

  readonly mastered =
    signal(0);

  readonly loading =
    signal(false);

  setCards(

    progressId: number,

    topic: string,

    cards: Flashcard[]

  ) {

    this.progressId.set(progressId);

    this.topic.set(topic);

    this.cards.set(cards);

    this.current.set(0);

    this.mastered.set(0);

  }

  next() {

    if (

      this.current() <

      this.cards().length - 1

    ) {

      this.current.update(v => v + 1);

    }

  }

  previous() {

    if (

      this.current() > 0

    ) {

      this.current.update(v => v - 1);

    }

  }

  markMastered() {

    this.mastered.update(v => v + 1);

  }

  clear() {

    this.progressId.set(null);

    this.topic.set('');

    this.cards.set([]);

    this.current.set(0);

    this.mastered.set(0);

    this.loading.set(false);

  }

}