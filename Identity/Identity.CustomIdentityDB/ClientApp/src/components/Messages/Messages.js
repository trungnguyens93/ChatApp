import React from "react";
import MessageItem from "../MessageItem";
import styles from "./Messages.module.css";

const Messages = (messages) => {
  const data = [
    {
      content:
        "How the hell am I supposed to get a jury to believe you when I am not even sure that I do?!",
      type: "sent",
    },
    {
      content:
        "When you're backed against the wall, break the god damn thing down.",
      type: "replies",
    },
    {
      content: "Excuses don't win championships.",
      type: "replies",
    },
    {
      content: "Oh yeah, did Michael Jordan tell you that?",
      type: "sent",
    },
    {
      content: "No, I told him that.",
      type: "replies",
    },
    {
      content: "What are your choices when someone puts a gun to your head?",
      type: "replies",
    },
    {
      content:
        "What are you talking about? You do what they say or they shoot you.",
      type: "sent",
    },
    {
      content:
        "Wrong. You take the gun, or you pull out a bigger one. Or, you call their bluff. Or, you do any one of a hundred and forty six other things.",
      type: "replies",
    },
  ];

  return (
    <div className={styles.messages}>
      <ul>
        {data.map((item, index) => {
          return <MessageItem key={index} {...item} />;
        })}
      </ul>
    </div>
  );
};

export default Messages;
