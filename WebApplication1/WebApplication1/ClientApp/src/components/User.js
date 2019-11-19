import React, { Component } from 'react';

export class User extends Component {
    static displayName = User.name;

    constructor(props) {
        super(props);
        this.state = {
            users: [],
            isLoaded: false,
        };
    }

    componentDidMount() {
        fetch('https://localhost:44304/api/users')
            .then(res => res.json())
            .then(json => {
                this.setState({
                    isLoaded: true,
                    items: json,
                })
            });
    }

    render() {
        var { isLoaded, items } = this.state;

        if (!isLoaded) {
            return <div>Loading...</div>
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
                        <th>Cell number</th>
                        <th>Birthday</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map(item =>
                        <tr key={item.id}>
                            <td>{item.id}</td>
                            <td>{item.firstName}</td>
                            <td>{item.lastName}</td>
                            <td>{item.email}</td>
                            <td>No info</td>
                            <td>No info</td>
                        </tr>
                    )}
                </tbody>
            </table>
            );
        }        
    }
}
