import React, { Component } from 'react';
import axios from 'axios'
import { Button, FormGroup, Input } from 'reactstrap';

export class Register extends Component {
    static displayName = Register.name;

    constructor(props) {
        super(props)

        this.state = {
            email: '',
            password: '',
            ConfirmPassword: '',
            firstName: '',
            lastName: '',
            cellNumber: ''
        }
    }

    changeHandler = (e) => {
        this.setState({ [e.target.name]: e.target.value })
    }

    submitHandler = e => {
        e.preventDefault()

        axios.post('https://localhost:44304/api/Identity/Register', this.state)
            .then(response => {
                console.log(response)
            })
            .catch(error => {
                console.log(error)
            })
    }

    render() {
        const { email, password, ConfirmPassword, firstName, lastName, cellNumber } = this.state
        return (
            <div>
                <form onSubmit={this.submitHandler}>
                    <FormGroup>
                        <Input type="text"
                            name="email"
                            id="exampleEmail"
                            placeholder="Email"
                            value={email}
                            onChange={this.changeHandler} />
                    </FormGroup>
                    <FormGroup>
                        <Input type="password"
                            name="password"
                            id="examplePassword"
                            placeholder="Password"
                            value={password}
                            onChange={this.changeHandler} />
                    </FormGroup>
                    <FormGroup>
                        <Input type="password"
                            name="ConfirmPassword"
                            id="examplePassword2"
                            placeholder="Confirm password"
                            value={ConfirmPassword}
                            onChange={this.changeHandler} />
                    </FormGroup>
                    <FormGroup>
                        <Input type="text"
                            name="firstName"
                            id="firstName"
                            placeholder="First Name"
                            value={firstName}
                            onChange={this.changeHandler} />
                    </FormGroup>
                    <FormGroup>
                        <Input type="text"
                            name="lastName"
                            id="lastName"
                            placeholder="Last Name"
                            value={lastName}
                            onChange={this.changeHandler} />
                    </FormGroup>
                    <FormGroup>
                        <Input type="text"
                            name="cellNumber"
                            id="cellNumber"
                            placeholder="Cell Number"
                            value={cellNumber}
                            onChange={this.changeHandler} />
                    </FormGroup>
                    <Button>Register</Button>
                </form>
            </div>
        );
    }
}