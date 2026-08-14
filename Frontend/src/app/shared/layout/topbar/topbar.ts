import { Component, inject } from '@angular/core';
import { LayoutService } from '../../../core/services/layout.service';
import { AuthService } from '../../../features/auth/services/auth.service';

@Component({
  selector: 'app-topbar',
  standalone: true,
  imports: [],
  templateUrl: './topbar.html',
  styleUrl: './topbar.css'
})
export class TopbarComponent {
  private readonly auth = inject(AuthService);
  user = this.auth.currentUser;

  readonly layout = inject(LayoutService);

  get greeting(): string {

    const hour = new Date().getHours();

    if (hour < 12) return 'Buenos días';
    if (hour < 18) return 'Buenas tardes';

    return 'Buenas noches';
  }

    get userInitial(): string {
    const name = this.auth.currentUser()?.name;

    return name
      ? name.trim().charAt(0).toUpperCase()
      : '?';
  }

}