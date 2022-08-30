export type TodoState = "Pending" | "Complete";

export interface Todo {
  id?: string;
  description: string;
  state: TodoState;
}
