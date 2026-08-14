export interface QuizAnswer {

  question: string;

  givenAnswer: string;

  correctAnswer: string;

}

export interface QuizSubmission {

  progressId: number;

  answers: QuizAnswer[];

}