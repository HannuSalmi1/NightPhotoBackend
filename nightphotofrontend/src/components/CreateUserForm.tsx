import React, { useState, FormEvent } from 'react';
import { Form, Button, Container, Row, Col } from 'react-bootstrap';

interface UserForm {
    firstname: string;
    lastname: string;
    username: string;
    email: string;
    password: string;
}

export default function CreateUserForm() {
    const [userForm, setUserForm] = useState<UserForm>({
        firstname: '',
        lastname: '',
        username: '',
        email: '',
        password: '',
    });

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const response = await fetch('https://backend:5000/api/Users/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(userForm),
        });

        // handle response
    };

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setUserForm({ ...userForm, [event.target.name]: event.target.value });
    };

    return (
        <Container className="bg-gray-900 min-h-screen d-flex align-items-center justify-content-center">
            <Form onSubmit={handleSubmit} className="w-100 max-w-sm">
                <h1 className="mb-4 text-center">Register</h1>
                <Form.Group as={Row} className="mb-3">
                    <Col>
                        <Form.Control
                            type="text"
                            placeholder="Firstname"
                            name="firstname"
                            value={userForm.firstname}
                            onChange={handleChange}
                        />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Col>
                        <Form.Control
                            type="text"
                            placeholder="Lastname"
                            name="lastname"
                            value={userForm.lastname}
                            onChange={handleChange}
                        />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Col>
                        <Form.Control
                            type="text"
                            placeholder="Username"
                            name="username"
                            value={userForm.username}
                            onChange={handleChange}
                        />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Col>
                        <Form.Control
                            type="email"
                            placeholder="Email"
                            name="email"
                            value={userForm.email}
                            onChange={handleChange}
                        />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3">
                    <Col>
                        <Form.Control
                            type="password"
                            placeholder="Password"
                            name="password"
                            value={userForm.password}
                            onChange={handleChange}
                        />
                    </Col>
                </Form.Group>
                <Button variant="primary" type="submit" className="w-100">
                    Create Account
                </Button>
            </Form>
        </Container>
    );
}