import React from "react";

import styles from "./Label.module.scss";

export interface LabelProps {
  text: string;
  htmlFor?: string;
  errorText?: React.ReactNode;
}

export function Label({
  errorText,
  htmlFor: property,
  text,
}: LabelProps): React.ReactElement {
  return (
    <label className={styles["label"]} htmlFor={property} id={property}>
      <span>{text}</span>
      {errorText && (
        <span
          className={styles["validation-error"]}
          role="alert"
          aria-live="polite"
        >
          {errorText}
        </span>
      )}
    </label>
  );
}
