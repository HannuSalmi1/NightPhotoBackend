import React, { useState, FormEvent } from 'react';
import { Form, Button, Container, Row, Col } from 'react-bootstrap';

interface SignInForm {

    username: string;

    password: string;
}

export default function CreateSignInForm() {
    const [signInForm, setSignInForm] = useState<SignInForm>({

        username: '',

        password: '',
    });

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        const response = await fetch('https://backend:5000/api/Users/', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(signInForm),
        });

        // handle response
    };

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSignInForm({ ...signInForm, [event.target.name]: event.target.value });
    };

    return (
        <Container className="bg-gray-900 min-h-screen d-flex align-items-center justify-content-center">
            <Form onSubmit={handleSubmit} className="w-100 max-w-sm">
                <h1 className="mb-4 text-center">Log In</h1>

                <Form.Group as={Row} className="mb-3">
                    <Col>
                        <Form.Control
                            type="text"
                            placeholder="Username"
                            name="username"
                            value={signInForm.username}
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
                            value={signInForm.password}
                            onChange={handleChange}
                        />
                    </Col>
                </Form.Group>
                <Button variant="primary" type="submit" className="w-100">
                    Log In
                </Button>
            </Form>
        </Container>
    );
}