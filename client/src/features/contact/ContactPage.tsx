import { Button, Typography } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { decrement, increment } from "./counterSlice";

export default function ContactPage() {
    // redux
    const dispatch = useAppDispatch();
    const { data, title } = useAppSelector(state => state.counter);    

    return (
        <>
            <Typography>
                {title}
            </Typography>
            <Typography>
                Contact page counter: {data}
            </Typography>
            <Button onClick={() => dispatch(decrement(1))} variant="contained" color="error">Decrement</Button>
            <Button onClick={() => dispatch(increment(1))} variant="contained" color="primary">Increment</Button>
            <Button onClick={() => dispatch(increment(5))} variant="contained" color="secondary">Increment by 5</Button>
        </>
    )
}