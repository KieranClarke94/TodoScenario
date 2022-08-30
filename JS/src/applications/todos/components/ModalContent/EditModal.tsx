import { Todo } from "../../models/todo";
import React, { useRef, useState } from "react";
import { Label, Input, Modal } from "../../../../components";

import styles from "./EditModal.module.scss";
import { useTodos } from "../../stores";

export default function EditModal(props: {
  todo?: Todo;
  onHide: () => void;
}): React.ReactElement {
  const [, todoActions] = useTodos();

  // Tracking name validation errors
  const [error, setError] = useState<string | undefined>();

  // References for the name and state inputs
  const nameReference = useRef<HTMLInputElement>(null);
  const checkboxReference = useRef<HTMLInputElement>(null);

  // Save the todo
  async function onConfirm(): Promise<void> {
    validate();
    if (!error) {
      const name = nameReference.current?.value;
      const completed = checkboxReference.current?.checked;

      const todo: Todo = {
        ...props.todo,
        description: name ?? "",
        state: completed ? "Complete" : "Pending",
      };

      if (props.todo) {
        await todoActions.update(todo);
      } else {
        await todoActions.create(todo);
      }

      props.onHide();
    }
  }

  // Validate the name input
  function validate(): void {
    const name = nameReference.current?.value;

    if (!name) {
      setError("This field is required");
    } else if (name.length > 250) {
      setError("This field has a max length of 250 characters");
    } else {
      setError(undefined);
    }
  }

  return (
    <Modal
      onClose={() => props.onHide()}
      modalContent={
        <div className={styles["modal-content"]}>
          <Label text="Name" errorText={error} />
          <Input
            innerRef={nameReference}
            property="name"
            defaultValue={props.todo?.description ?? ""}
            onChange={() => validate()}
            hasError={!!error}
          ></Input>
          <label className={styles["completed"]}>
            <input
              id="completed"
              ref={checkboxReference}
              type="checkbox"
              defaultChecked={props.todo?.state == "Complete"}
            ></input>
            Completed
          </label>
        </div>
      }
      headerText={props.todo ? "Edit todo" : "Add new todo"}
      onConfirm={onConfirm}
    />
  );
}
