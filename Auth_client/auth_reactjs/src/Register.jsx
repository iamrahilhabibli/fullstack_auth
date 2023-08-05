import React, { useState } from "react";
import styles from "./Register.module.css";
import axios from "axios";

export function Register() {
  const [formData, setFormData] = useState({
    fullname: "",
    username: "",
    email: "",
    password: "",
  });

  const handleInputChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(
        "https://localhost:7075/api/Accounts/Register",
        formData
      );

      if (response.status === 200) {
        alert("User registered successfully!");
      }
    } catch (error) {
      console.error("There was an error!", error);
    }
  };

  return (
    <form className={styles.formContainer} onSubmit={handleSubmit}>
      <input
        name="fullname"
        value={formData.fullname}
        onChange={handleInputChange}
        placeholder="Full Name"
      />
      <input
        name="username"
        value={formData.username}
        onChange={handleInputChange}
        placeholder="Username"
      />
      <input
        name="email"
        value={formData.email}
        onChange={handleInputChange}
        placeholder="Email"
        type="email"
      />
      <input
        name="password"
        value={formData.password}
        onChange={handleInputChange}
        placeholder="Password"
        type="password"
      />
      <button type="submit">Register</button>
    </form>
  );
}
