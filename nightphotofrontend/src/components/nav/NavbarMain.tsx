import React from 'react';
import { Navbar, Nav, NavDropdown, Container } from 'react-bootstrap';
import  '../../App.css';

function NavbarMain() {
  return (
    <>
      <Navbar bg="dark" data-bs-theme="dark" fixed="top">
        <Container>
          <Navbar.Brand href="#home" className="navbar-brand-custom">NightPhoto</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link href="/">Home</Nav.Link>
            <Nav.Link href="Login">Login</Nav.Link>
            <Nav.Link href="Register">Register</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
      <br />

</>
  )
}

export default NavbarMain;