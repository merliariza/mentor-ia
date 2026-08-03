import { Component, inject } from '@angular/core';
import { NgClass } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { LayoutService } from '../../../core/services/layout.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    NgClass,
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class SidebarComponent {

  readonly layout = inject(LayoutService);

  navigation = [
    { label: 'Inicio', route: '/dashboard' },
    { label: 'Mentor IA', route: '/chat' },
    { label: 'Flashcards', route: '/flashcards' },
    { label: 'Quiz', route: '/quiz' }
  ];

  close() {
    this.layout.closeSidebar();
  }

}