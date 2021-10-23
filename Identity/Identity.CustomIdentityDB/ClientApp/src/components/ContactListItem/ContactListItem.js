import React from "react";
import PropTypes from "prop-types";
import image from "../../asserts/images/avatar.svg";
import styles from "./ContactListItem.module.css";

const ContactListItem = (props) => {
  return (
    <li
      className={
        styles.contact + " " + (props.status === "active" ? styles.active : "")
      }
    >
      <div className={styles.wrap}>
        <span className={styles.status + " " + styles.online}></span>

        <img src={image} alt="" />

        <div className={styles.meta}>
          <p className={styles.name}>{props.name}</p>
        </div>
      </div>
    </li>
  );
};

ContactListItem.prototype = {
  id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
  name: PropTypes.string,
  status: PropTypes.string,
};

export default ContactListItem;
