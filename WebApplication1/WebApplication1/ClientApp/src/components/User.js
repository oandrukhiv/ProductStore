import React, { Component } from 'react';
import axios from 'axios'

const API = 'https://localhost:44304/api/users';
const source = localStorage.getItem('myValueInLocalStorage');

export class User extends Component {
    static displayName = User.name;

    constructor(props) {
        super(props);
        this.state = {
            users: [],
            isLoaded: false,
        };
    }

    async componentDidMount() {
        const { data } = await axios.get(API, {
            headers: {
                'Authorization': 'Bearer ' + source
            }
        })
        this.setState({ items: data, isLoaded: true }, () => {
            console.log(this.state.items)
            return;
        })
    }

    render() {
        const { isLoaded, items = [] } = this.state;
        
        if (!isLoaded) {
            return <div>Loading.cccc..</div>
        }
        else {
            return (
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>First Name</th>
                            <th>Last Name</th>
                            <th>Email</th>
                            <th>Cell</th>
                            <th>Role</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map(item =>
                            <tr key={item.id}>
                                <td>{item.id}</td>
                                <td>{item.firstName}</td>
                                <td>{item.lastName}</td>
                                <td>{item.email}</td>
                                <td>{item.cellNumber}</td>
                                <td>{item.role}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            );
        }        
    }
}
