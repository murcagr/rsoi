    import configureStore from '../store/configureStore.js';

const updateCotsType = 'UPDATE_CATS';
const decrementCountType = 'DECREMENT_COUNT';
const AddCotType = 'ADD_CAT';
const initialState = { cots: [] };

export const actionCreators = {
    cotsRequest: (page) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/ownercatfood/?page=' + page)
            .then(res => res.json())
            .then((data) => {
                dispatch(actionCreators.updateCots(data));
                console.log("cots data from action: ", data)
            })
            .catch(console.log)
    },

    updateCots: (cots) => ({ type: updateCotsType, cots: cots }),

    cofAdd: (catOwnerFood) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/ownercats', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

            },
            body: JSON.stringify({
                cat: {
                    name: catOwnerFood.nameCat,
                    breed: catOwnerFood.breed
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
        fetch('https://localhost:5049/api/gw/ownercats/' + delCat.id, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

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