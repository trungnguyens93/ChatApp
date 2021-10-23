import React from "react";
import PropTypes from "prop-types";
import styles from "./ContentProfile.module.css";

const ContentProfile = (props) => {
  return (
    <div className={styles.contact__profile}>
      <img src={props.avatar} alt="" />
      <p>{props.name}</p>
    </div>
  );
};

ContentProfile.propTypes = {
  name: PropTypes.string,
  avatar: PropTypes.string,
};

export default ContentProfile;
