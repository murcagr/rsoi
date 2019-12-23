import configureStore from '../store/configureStore.js';
import { GATEWAY_ADDR } from '../appconfig';
import { OAUTH_ADDR } from '../appconfig';
const updateCotsType = 'UPDATE_CATS';
const decrementCountType = 'DECREMENT_COUNT';
const AddCotType = 'ADD_CAT';
const initialState = { cots: [] };

export const actionCreators = {
    cotsRequest: (page) => (dispatch) => {
        fetch(`${GATEWAY_ADDR}/api/gw/ownercatfood/?page=` + page, {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `${sessionStorage.getItem("scheme")} ${sessionStorage.getItem("auth_token")}`
            }
        })
            .then(res => res.json())
            .then((data) => {
                dispatch(actionCreators.updateCots(data));
                console.log("cots data from action: ", data)
            })
            .catch(console.log)
    },

    updateCots: (cots) => ({ type: updateCotsType, cots: cots }),

    cofAdd: (catOwnerFood) => (dispatch) => {
        fetch(`${GATEWAY_ADDR}/api/gw/ownercats`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `${sessionStorage.getItem("scheme")} ${sessionStorage.getItem("auth_token")}`
            },
            body: JSON.stringify({
                cat: {
                    name: catOwnerFood.nameCat,
                    breed: catOwnerFood.breed,
                    foodId: catOwnerFood.foodId
                },
                owner: {
                    name: catOwnerFood.nameOwner,
                    age: catOwnerFood.age,
                    city: catOwnerFood.city
                }
            })
        })
            .then(res => res.json())
            .catch(console.log)
    },
    cofsDelete: (delCat) => (dispatch) => {
        fetch(`${GATEWAY_ADDR}/api/gw/ownercats/` + delCat.id, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `${sessionStorage.getItem("scheme")} ${sessionStorage.getItem("auth_token")}`
            },

        })
            .then(res => res.json())
            .catch(console.log)
    }

};

export const cotsReducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateCotsType) {
        return { cots: action.cots };
    }

    return state;
};

export default cotsReducer