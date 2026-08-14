import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { NgClass } from '@angular/common';

import { LayoutService } from '../../../core/services/layout.service';
import { AuthService } from '../../../features/auth/services/auth.service';
import { ChatService } from '../../../core/services/chat.service';

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
  readonly auth = inject(AuthService);

  private readonly router = inject(Router);
  private readonly chatService = inject(ChatService);


  userMenuOpen = false;


  navigation = [
    { label: 'Inicio', route: '/dashboard' },
    { label: 'Mentor IA', route: '/chat' },
    { label: 'Flashcards', route: '/flashcards' },
    { label: 'Quiz', route: '/quiz' }
  ];


  get userInitial(): string {

    const name =
      this.auth.currentUser()?.name;

    return name
      ? name.trim().charAt(0).toUpperCase()
      : '?';

  }


  get userName(): string {

    const user =
      this.auth.currentUser();

    return (
      user?.name ||
      user?.userName ||
      'Usuario'
    );

  }


  get userEmail(): string {

    return this.auth.currentUser()?.email || '';

  }


  toggleUserMenu(): void {

    this.userMenuOpen =
      !this.userMenuOpen;

  }


  logout(): void {

    this.chatService.clearConversation();

    this.auth.logout();

    this.userMenuOpen = false;

    this.layout.closeSidebar();

    this.router.navigateByUrl('/login');

  }


  close(): void {

    this.layout.closeSidebar();

  }

}