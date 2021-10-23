import React from "react";
import PropTypes from "prop-types";
import image from "../../asserts/images/avatar.svg";
import styles from "./MessageItem.module.css";

const MessageItem = (props) => {
  return (
    <li
      className={
        styles.message__li +
        " " +
        (props.type === "sent" ? styles.sent : styles.replies)
      }
    >
      <img src={image} alt="" />
      <p>{props.content}</p>
    </li>
  );
};

MessageItem.prototype = {
  id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
  content: PropTypes.string,
  type: PropTypes.string,
};

export default MessageItem;
