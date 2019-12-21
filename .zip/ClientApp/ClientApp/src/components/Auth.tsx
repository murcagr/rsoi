import React, { Component } from 'react';

class CatList extends Component {

    render() {
        
        let Cats = this.props.cats.map((cats) => {
            return <div key= "5">{cats.name}</div>;
        });

        return (
            <div id="cats" className="CatsList">
                <ul>{Cats}</ul>
            </div>
        );
    }

}

export default CatList