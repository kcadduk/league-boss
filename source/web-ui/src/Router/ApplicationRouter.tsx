import {RouteObject, createBrowserRouter, RouterProvider} from 'react-router-dom'
import {ApplicationRootLayout} from "../ApplicationRoot/ApplicationRootLayout.tsx";

const routes: RouteObject[] = [
    {
        path: '',
        element: <ApplicationRootLayout/>,
        children: [
        ],
    },
];

const ApplicationRouter = () => {
    const router = createBrowserRouter(routes, {
        basename: '/play'
    });
    return <RouterProvider router={router}/>;
}
export default ApplicationRouter;