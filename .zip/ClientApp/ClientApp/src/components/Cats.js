import React, { Component } from 'react';
import { actionCreators as catsActions } from '../reducers/Cats';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

class Cats extends Component {

    componentDidMount() {
        this.props.actions.catsRequest();
    }

    render() {
        return (
            <div>
                {this.props.cats.length != 0 ? this.props.cats[0].name : ""}
                <button onClick={() => this.props.actions.catsAdd("LOSHARA")}>
                    POST
                </button>
            </div>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(catsActions, dispatch)
    };
}

function mapStateToProps(state) {
    return {
        cats: state.cats.cats
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(Cats);