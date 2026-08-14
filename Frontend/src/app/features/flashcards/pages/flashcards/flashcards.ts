import { Component, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FlashcardStateService } from '../../../../core/services/flashcard-state.service';

@Component({
  selector: 'app-flashcards',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './flashcards.html',
  styleUrls: ['./flashcards.css']
})
export class FlashcardsComponent {

  readonly state =
    inject(FlashcardStateService);

  readonly cards =
    this.state.cards;

  readonly currentIndex =
    this.state.current;

  readonly mastered =
    this.state.mastered;

  flipped =
    signal(false);

  readonly currentCard =
    computed(() =>

      this.cards()[this.currentIndex()]

    );

  flipCard() {

    this.flipped.update(v => !v);

  }

  next() {

    this.flipped.set(false);

    this.state.next();

  }

  previous() {

    this.flipped.set(false);

    this.state.previous();

  }

  masteredCard() {

    this.state.markMastered();

    this.next();

  }

}