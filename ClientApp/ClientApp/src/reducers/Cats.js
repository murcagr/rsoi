import configureStore from '../store/configureStore.js';
import { browserHistory, Router, Route } from 'react-router';
import { push } from 'connected-react-router';
const updateCatsType = 'UPDATE_CATS';
const decrementCountType = 'DECREMENT_COUNT';
const AddCatType = 'ADD_CAT';
const initialState = { cats: [] };

export const actionCreators = {
    catsRequest: () => (dispatch) => {
        fetch('https://localhost:5049/api/gw/cats')
            .then(res => res.json())
            .then((data) => {
                dispatch(actionCreators.updateCats(data));
                console.log("cats data from action: ", data)
            })
            .catch(console.log)
    },

    updateCats: (cats) => ({ type: updateCatsType, cats: cats }),


    catsAdd: (newCat) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/cats', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `${sessionStorage.getItem("scheme")} ${sessionStorage.getItem("auth_token")}`

            },
            body: JSON.stringify({
                name: newCat.name,
                breed: newCat.breed,
                foodId: newCat.foodId
            })
        })
            .then(res => res.json())
            .catch(console.log)
    },

    catsDelete: (delCat) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/cats/' + delCat.id.id, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `${sessionStorage.getItem("scheme")} ${sessionStorage.getItem("auth_token")}`
            },
            
        })
            .then(res => res.json())
            .catch(console.log)
    },

    requestOAuth: (code) => (dispatch) => {
        
        var details = {
            'client_id': 'spa',
            'client_secret': 'secret',
            'grant_type': 'authorization_code',
            'code': code,
            'redirect_uri': 'https://localhost:5100/oacallback'
        };

        var formBody = [];
        for (var property in details) {
            var encodedKey = encodeURIComponent(property);
            var encodedValue = encodeURIComponent(details[property]);
            formBody.push(encodedKey + "=" + encodedValue);
        }
        var formBodyString = formBody.join("&");

        fetch(`http://localhost:5010/connect/token`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: formBodyString
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                console.log(data);
                sessionStorage.setItem('auth_token', data.access_token);
                sessionStorage.setItem('refresh_token', data.refresh_token);
                sessionStorage.setItem('expires_in', data.expires_in.toString());
                sessionStorage.setItem('scheme', "Bearer");
                sessionStorage.setItem('username', "admin");

                let timeout = (data.expires_in - 10) * 1000;
                setTimeout(function () { actionCreators.refreshOAuth(); }, timeout);

                dispatch(actionCreators.moveToMainMenu());
            })
            .catch(error => {
                console.log(error);
            });
    },

    moveToMainMenu: () => (dispatch) => { dispatch(push('/cats')); },

    refreshOAuth: () => {
        let refresh_token = sessionStorage.getItem('refresh_token') != undefined ? sessionStorage.getItem('refresh_token') : "";

        var details = {
            'client_id': 'spa',
            'client_secret': 'secret',
            'grant_type': 'refresh_token',
            'refresh_token': refresh_token,
            'redirect_uri': 'https://localhost:5100/oacallback'
        };

        var formBody = [];
        for (var property in details) {
            var encodedKey = encodeURIComponent(property);
            var encodedValue = encodeURIComponent(details[property]);
            formBody.push(encodedKey + "=" + encodedValue);
        }
        var formBodyString = formBody.join("&");

        fetch(`http://localhost:5010/connect/token`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: formBodyString
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                sessionStorage.setItem('auth_token', data.access_token);
                sessionStorage.setItem('refresh_token', data.refresh_token);
                sessionStorage.setItem('expires_in', data.expires_in.toString());
                sessionStorage.setItem('scheme', "Bearer");
                sessionStorage.setItem('username', "a");

                setTimeout(function () { actionCreators.refreshOAuth(); }, (data.expires_in - 10) * 1000);
            })
            .catch((error) => {
                console.log(error);
            });

    },
};

export const catsReducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateCatsType) {
        return { cats: action.cats };
    }

    return state;
};

export default catsReducer