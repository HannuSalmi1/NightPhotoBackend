import React from 'react';
import { Navbar, Nav, NavDropdown, Container } from 'react-bootstrap';
import  '../App.css';

function NavbarMain() {
  return (
    <>
      <Navbar bg="dark" data-bs-theme="dark" fixed="top">
        <Container>
          <Navbar.Brand href="#home" className="navbar-brand-custom">Navbar</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link href="#home">Home</Nav.Link>
            <Nav.Link href="#features">Features</Nav.Link>
            <Nav.Link href="#pricing">Pricing</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
      <br />

</>
  )
}

export default NavbarMain;