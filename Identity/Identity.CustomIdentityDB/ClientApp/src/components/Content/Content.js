import React from "react";
import ContentProfile from "../ContentProfile";
import MessageInput from "../MessageInput";
import Messages from "../Messages/Messages";
import image from "../../asserts/images/avatar.svg";
import styles from "./Content.module.css";

const Content = () => {
  const profile = {
    name: "Trung Nguyen",
    avatar: image,
  };

  return (
    <div className={styles.content}>
      <ContentProfile {...profile} />

      <div></div>

      <Messages />

      <div></div>

      <MessageInput />
    </div>
  );
};

export default Content;
