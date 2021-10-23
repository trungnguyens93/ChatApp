import React from "react";
import styles from "./MessageInput.module.css";

const MessageInput = () => {
  return (
    <div className={styles.message__input}>
      <div className={styles.wrap}>
        <input type="text" placeholder="Write your message..." />
        <button className={styles.submit}>
          <i className="fa fa-paper-plane" aria-hidden="true"></i>
        </button>
      </div>
    </div>
  );
};

export default MessageInput;
