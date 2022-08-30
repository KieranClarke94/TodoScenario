import React, { useEffect, useState } from "react";

import styles from "./Input.module.scss";

export interface InputProps {
  property: string;
  defaultValue?: string | number;
  value?: string | number;
  innerRef?: React.Ref<HTMLInputElement>;
  hasError?: boolean;
  onClick?: (event: React.MouseEvent<HTMLDivElement>) => void;
  onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

export function Input({
  property,
  innerRef,
  defaultValue,
  value,
  hasError,
  onChange,
  onClick,
}: InputProps): React.ReactElement {
  const [innerValue, setInnerValue] = useState<string | number | undefined>(
    value
  );

  useEffect(() => {
    setInnerValue(value);
  }, [value]);

  const handleOnChange = (event: React.ChangeEvent<HTMLInputElement>): void => {
    if (onChange) {
      setInnerValue(event.currentTarget.value);
    }
  };

  return (
    <input
      className={`${styles["input"]} ${hasError ? styles["error"] : ""}`}
      id={property}
      type="text"
      name={property}
      ref={innerRef}
      defaultValue={defaultValue}
      {...(innerValue !== undefined && { value: innerValue })}
      onClick={onClick}
      onChange={handleOnChange}
      dir="auto"
    />
  );
}
