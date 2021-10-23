import React from "react";
import PropTypes from "prop-types";
import ContactListItem from "../ContactListItem";
import styles from "./ContactList.module.css"

const ContactList = ({ contacts }) => {
  const data = [
    {
      name: "Louis Litt",
      status: "active"
    },
    {
      name: "Louis Litt",
    },
    {
      name: "Louis Litt",
    },
    {
      name: "Louis Litt",
    },
  ];

  return (
    <div className={styles.contacts}>
      <ul>
        {data.map((item, index) => {
          return <ContactListItem key={index} {...item} />;
        })}
      </ul>
    </div>
  );
};

ContactList.prototype = {
  contacts: PropTypes.arrayOf(PropTypes.shape({})),
};

export default ContactList;
