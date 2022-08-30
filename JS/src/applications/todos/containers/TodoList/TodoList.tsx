import TodoListItem from "../../components/TodoItem/TodoItem";
import { TodoState } from "../../models/todo";
import { useTodos } from "../../stores";
import React from "react";

import styles from "./TodoList.module.scss";

export default function TodoList(props: {
  type: TodoState;
}): React.ReactElement {
  const [todoStore] = useTodos();

  const { type } = props;

  return (
    <div className={styles["todo-list"]}>
      <h3>{type}</h3>
      <ul>
        {todoStore.state
          .filter((x) => x.state == type)
          .map((x) => (
            <TodoListItem todo={x} />
          ))}
      </ul>
    </div>
  );
}
