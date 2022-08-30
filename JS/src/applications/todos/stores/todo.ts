import { createStore, StoreActionApi, createHook } from "react-sweet-state";
import { Todo } from "../models/todo";
import axios from "axios";

// State type
type State = {
  state: Array<Todo>;
  fetching: boolean;
  fetched: boolean;
};

// Initial state for the store
const initialState = (): State => {
  return {
    state: [],
    fetched: false,
    fetching: false,
  };
};

// Actions to be applied against the store
const actions = {
  // Fetch the todos
  fetch:
    () =>
    async ({ setState }: StoreActionApi<State>) => {
      setState({ fetching: true });

      try {
        const { data } = await axios.get<Array<Todo>>(
          "http://localhost:8080/api/todos",
          {
            headers: {
              Accept: "application/json",
            },
          }
        );

        setState({
          fetched: true,
          fetching: false,
          state: data,
        });
      } catch (error) {
        return "An unexpected error occurred";
      }
    },
  // Create a new todo
  create:
    (todo: Todo) =>
    async ({ getState, setState }: StoreActionApi<State>) => {
      const current = getState().state;

      try {
        const { data } = await axios.post<Todo>(
          "http://localhost:8080/api/todos",
          todo,
          {
            headers: {
              "Content-Type": "application/json",
              Accept: "application/json",
            },
          }
        );

        current.push(data);

        setState({
          fetched: true,
          fetching: false,
          state: current,
        });
      } catch (error) {
        return "An unexpected error occurred";
      }
    },
  // Update a todo
  update:
    (todo: Todo) =>
    async ({ getState, setState }: StoreActionApi<State>) => {
      const current = getState().state;

      try {
        const { data } = await axios.put<Todo>(
          `http://localhost:8080/api/todos/${todo.id}`,
          todo,
          {
            headers: {
              "Content-Type": "application/json",
              Accept: "application/json",
            },
          }
        );

        const idx = current.findIndex((x) => x.id == todo.id);
        if (idx > -1) {
          current[idx] = data;
        }

        setState({
          fetched: true,
          fetching: false,
          state: JSON.parse(JSON.stringify(current)),
        });
      } catch (error) {
        return "An unexpected error occurred";
      }
    },
  /**
   * Resets the store to it's initial state.
   */
  reset:
    () =>
    ({ setState }: StoreActionApi<State>) => {
      setState(initialState());
    },
};

export const TodoStore = createStore<State, typeof actions>({
  name: "todos",
  initialState: initialState(),
  actions,
});

export const useTodos = createHook(TodoStore);
