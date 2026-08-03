export interface ChatResponse {
  user: string;
  question: string;
  topic?: string;
  answer: string;
  progressId?: number;
}