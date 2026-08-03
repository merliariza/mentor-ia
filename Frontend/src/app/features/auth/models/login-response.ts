export interface LoginResponse {

  codeb: string;

  message: string;

  isAuthenticated: boolean;

  email: string;

  name: string;

  userName: string;

  token: string;

  refreshTokenExpiration: string;

}