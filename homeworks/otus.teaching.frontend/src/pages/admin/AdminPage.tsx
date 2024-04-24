import React from 'react';
import {useAppSelector} from "@hooks/reducs";


export const AdminPage = () => {

    const userName = useAppSelector((state) => state.auth.userName)

    return (
        <div>
            Very very secret page for user {userName}
        </div>
    );
}