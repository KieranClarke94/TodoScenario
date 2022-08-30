import React, { useState } from "react";
import EditModal from "../ModalContent/EditModal";
import { Todo } from "../../models/todo";

import styles from "./TodoItem.module.scss";

export default function TodoListItem(props: {
  todo: Todo;
}): React.ReactElement {
  const { todo } = props;
  const [show, setShow] = useState<boolean>(false);

  return (
    <>
      <li
        key={todo.id}
        className={styles["todo-item"]}
        onClick={() => setShow(true)}
      >
        <div>{todo.description}</div>
      </li>
      {show && <EditModal todo={todo} onHide={() => setShow(false)} />}
    </>
  );
}
