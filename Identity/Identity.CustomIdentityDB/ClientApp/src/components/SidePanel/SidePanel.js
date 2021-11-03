import React from "react";
import Profile from "../Profile";
import SearchTextbox from "../SearchTextbox";
import ContactList from "../ContactList";

import style from "./SidePanel.module.css";

const SidePanel = (props) => {
  const user = {
    username: "Trung Nguyen",
    status: "online",
  };

  return (
    <div className={style.sidepanel}>
      <Profile {...user} />

      <div></div>

      <SearchTextbox />

      <div></div>

      <ContactList />
    </div>
  );
};

export default SidePanel;
