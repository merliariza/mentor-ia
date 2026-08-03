import { Routes } from '@angular/router';

import { DashboardLayoutComponent } from './shared/layout/dashboard-layout/dashboard-layout';

import { HomeComponent } from './features/dashboard/pages/home/home';
import { ChatComponent } from './features/chat/pages/chat/chat';
import { QuizComponent } from './features/quiz/pages/quiz/quiz';
import { FlashcardsComponent } from './features/flashcards/pages/flashcards/flashcards';

import { LoginComponent } from './features/auth/pages/login/login';
import { RegisterComponent } from './features/auth/pages/register/register';

import { authGuard } from './features/auth/guards/auth.guard';


export const routes: Routes = [

  {
    path: 'login',
    component: LoginComponent
  },

  {
    path: 'register',
    component: RegisterComponent
  },


  {
    path: '',
    component: DashboardLayoutComponent,

    canActivate: [
      authGuard
    ],

    children: [

      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },

      {
        path: 'dashboard',
        component: HomeComponent
      },

      {
        path: 'chat',
        component: ChatComponent
      },

      {
        path: 'quiz',
        component: QuizComponent
      },

      {
        path: 'flashcards',
        component: FlashcardsComponent
      }

    ]

  },


  {
    path: '**',
    redirectTo: 'login'
  }

];