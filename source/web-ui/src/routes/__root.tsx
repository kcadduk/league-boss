import {Outlet, createRootRouteWithContext} from "@tanstack/react-router";

import {QueryClient} from "@tanstack/react-query";

export interface RouterContext {
    queryClient: QueryClient
}

export const Route = createRootRouteWithContext<RouterContext>()({
  component: RootComponent
});

function RootComponent() {
  return (
    <>
      <Outlet />
    </>
  );
}
