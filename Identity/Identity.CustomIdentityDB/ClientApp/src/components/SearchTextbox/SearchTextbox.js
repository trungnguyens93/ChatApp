import React from "react";
import "font-awesome/css/font-awesome.min.css";
import style from "./SearchTextbox.module.css";

const SearchTextbox = () => {
  return (
    <div className={style.search}>
      <label>
        <i className="fa fa-search" aria-hidden="true"></i>
      </label>
      <input type="text" placeholder="Search contacts..." />
    </div>
  );
};

export default SearchTextbox;
