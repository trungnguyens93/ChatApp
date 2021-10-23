import React from "react";
import PropTypes from "prop-types";
import image from "../../asserts/images/avatar.svg";
import style from "./Profile.module.css";

const Profile = (props) => {
  return (
    <div className={style.profile}>
      <div className={style.wrap}>
        <img src={image} className={props.status} alt="" />
        <p>{props.username}</p>
      </div>
    </div>
  );
};

Profile.prototype = {
  id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
  username: PropTypes.string,
  status: PropTypes.string,
};

export default Profile;
