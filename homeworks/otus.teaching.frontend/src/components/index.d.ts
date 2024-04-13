declare module '*.mp3';

declare module '*.jpg' {
  const value: unknown;
  export default value;
}
declare module '*.png' {
  const value: unknown;
  export default value;
}

declare module "*.svg" {
  import * as React from "react";

  const ReactComponent: React.FunctionComponent<
      React.SVGProps<SVGSVGElement> & { title?: string }
  >;

  export default ReactComponent;
}