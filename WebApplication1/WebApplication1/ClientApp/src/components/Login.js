import React, { Component } from 'react';
import axios from 'axios'
import { Button, FormGroup, Input } from 'reactstrap';


export class Login extends Component {
    static displayName = Login.name;
    constructor(props) {
        super(props)

        this.state = {
            email: '',
            password: ''
        }
    }

    changeHandler = (e) => {
        this.setState({ [e.target.name]: e.target.value })
    }

    submitHandler = e => {
        e.preventDefault()

        axios.post('https://localhost:44304/api/Identity/Login', this.state)
            .then(response => {
                console.log(response)
            })           
            .catch(error => {
                console.log(error)
            })
    }

    render() {
        const { email, password } = this.state
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
                    <Button>Submit</Button>
                </form>
            </div>            
        );
    }
}
