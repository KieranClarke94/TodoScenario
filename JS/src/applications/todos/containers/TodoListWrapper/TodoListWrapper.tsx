import { useTodos } from "../../stores";
import React, { useEffect, useState } from "react";
import TodoList from "../TodoList/TodoList";

import styles from "./TodoListWrapper.module.scss";
import EditModal from "../../components/ModalContent/EditModal";

export default function TodoListWrapper(): React.ReactElement {
  const [todoStore, todoActions] = useTodos();

  // State to handle whether or not to show the modal
  const [show, setShow] = useState<boolean>(false);

  // Reset store on unmount
  useEffect(() => {
    return todoActions.reset;
  }, [todoActions]);

  // Fetch if not already fetched
  useEffect(() => {
    if (!todoStore.fetched) {
      todoActions.fetch();
    }
  }, [todoActions, todoStore.fetched]);

  return (
    <>
      <div className={styles["todo-wrapper"]}>
        <div>
          <h2>Todo Items</h2>
          <button onClick={() => setShow(true)}>Add new todo</button>
        </div>
        <div className={styles["list-wrapper"]}>
          <TodoList type="Pending" />
          <TodoList type="Complete" />
        </div>
      </div>
      {show && <EditModal onHide={() => setShow(false)} />}
    </>
  );
}
