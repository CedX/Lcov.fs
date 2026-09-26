namespace Belin.Lcov

open Microsoft.VisualStudio.TestTools.UnitTesting

// The assembly properties.
[<assembly: Parallelize(Scope = ExecutionScope.MethodLevel)>]
do ()
