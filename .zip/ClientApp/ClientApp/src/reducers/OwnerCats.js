    import configureStore from '../store/configureStore.js';

const updateOwnersType = 'UPDATE_OWNERS';
const decrementCountType = 'DECREMENT_COUNT';
const AddOwnerType = 'ADD_OWNER';
const initialState = { owners: [] };

export const actionCreators = {
    
    ownerCatsGet: (currentUserReq) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/ownercats/' + currentUserReq.id, {
            method: 'Get',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

            },
            
        })
            .then(res => res.json())
            .catch(console.log)
    }

};

export const ownersReducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateOwnersType) {
        return { owners: action.owners };
    }

    return state;
};

export default ownersReducer