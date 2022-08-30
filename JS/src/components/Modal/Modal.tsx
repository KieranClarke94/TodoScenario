import React, { useEffect } from "react";

import styles from "./Modal.module.scss";

export interface ModalProps {
  onClose: () => void;
  onConfirm: () => void;
  modalContent: JSX.Element;
  headerText: string;
}

export const Modal: React.FC<ModalProps> = (props) => {
  const { onClose, modalContent, headerText } = props;

  return (
    <>
      <div className={styles["backdrop"]} onClick={onClose}></div>
      <div
        className={styles["wrapper"]}
        aria-modal
        aria-labelledby={headerText}
        tabIndex={-1}
        role="dialog"
      >
        <div className={styles["styled-modal"]}>
          <div className={styles["header"]}>
            <div className={styles["header-text"]}>{headerText}</div>
            <button className={styles["close-button"]} onClick={onClose}>
              X
            </button>
          </div>
          <div className={styles["content"]}>{modalContent}</div>
          <div className={styles["footer"]}>
            <button onClick={props.onConfirm}>Confirm</button>
            <button onClick={props.onClose}>Close</button>
          </div>
        </div>
      </div>
    </>
  );
};
