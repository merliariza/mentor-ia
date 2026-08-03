export interface ChatRequest {
  question: string;
  user: {
    id: number;
    fullName: string;
  };
}