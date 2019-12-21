import React, { Component } from 'react';
import { actionCreators as catsActions } from '../reducers/Cats';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import * as queryString from 'query-string';

class Oauth extends Component {

    componentDidMount()  {
        const values = queryString.parse(this.props.location.search);
        this.props.actions.requestOAuth(values.code)
    }


    render() {
        return (
            <div> LOADING
            </div>

        );
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

export default connect(mapStateToProps, mapDispatchToProps)(Oauth);