import React, { Component } from 'react';

export class Product extends Component {
    static displayName = Product.name;

    constructor(props) {
        super(props);
        this.state = {
            products: [],
            isLoaded: false,
        };
        this.routeChange = this.routeChange.bind(this);
    }
    routeChange() {
        let path = `/CreateProduct`;
        this.props.history.push(path);
    }
    componentDidMount() {
        fetch('https://localhost:44304/api/products')
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
                            <th>Name</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map(item =>
                            <tr key={item.id}>
                                <td>{item.id}</td>
                                <td>{item.name}</td>
                                <td>{item.price}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            );
        }
    }
}
